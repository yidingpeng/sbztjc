using FreeSql;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Entities.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RW.PMS.DataImport
{
    public partial class FormImportProjectInfo : Form
    {
        public FormImportProjectInfo()
        {
            InitializeComponent();
        }

        DataTable table = null;

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = this.textBox1.Text;

                if (!File.Exists(filename))
                {
                    MessageBox.Show("文件不存在，无法导入。");
                    return;
                }

                table = NPOIExcel.ExcelToDataTable(filename, 0, 0);
                this.dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (table == null) return;


            string connectionString = "server=192.168.0.53;uid=root;pwd=RWrw@!$%*123456;database=rw.plm";

            IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.MySql, connectionString)
                .UseAutoSyncStructure(true) //自动迁移实体的结构到数据库
                .Build();


            //fsql.GetRepository<MyClass>();
            var clientRepo = fsql.GetRepository<Client_CompanyEntity>();
            var projRepo = fsql.GetRepository<ProjectBasicsEntity>();
            var logsRepo = fsql.GetRepository<ProjectDynamicEntity>();
            var dictRepo = fsql.GetRepository<DictionaryEntity>();
            var contactRepo = fsql.GetRepository<Project_ContactsInfo>();
            var userRepo = fsql.GetRepository<UserEntity>();
            var userExtRepo = fsql.GetRepository<UserExtenderEntity>();

            //初始化字典
            //1、字典：项目状态
            //var dictNames = new string[] {
            //        "计划管理员", "项目经理", "产线负责人", "产品经理", "机械人员", "电气人员", "软件人员"
            //    };
            //var statusDict = GetDictionary(dictRepo, "项目状态", dictNames);
            //2、字典：负责板块
            var fzbkNames = new string[] {
                "计划管理员", "项目经理", "产线负责人", "产品经理", "机械人员", "电气人员", "软件人员"
            };
            var fzbkDict = GetDictionary(dictRepo, "负责板块", fzbkNames);
            //3、字典：项目状态
            var statusNames = new string[] {
                "退货", "未开工", "在产", "已入库，待发货", "已发货，待验收", "已验收", "运维中", "已结题", "暂停", "取消","已完成","进行中",
            };
            var statusDict = GetDictionary(dictRepo, "项目状态", statusNames);
            //4、字典：项目分类
            var typeNames = new string[] { "线索项目", "潜在项目", "在产项目" };
            var typeDict = GetDictionary(dictRepo, "项目分类", typeNames);

            //5、字典：业务板块
            var businessNames = new string[] { "铁路产品", "军工产品", "培训产品", "维保市场" };
            var businessDict = GetDictionary(dictRepo, "业务板块", businessNames);


            fsql.Ado.ExecuteNonQuery("truncate table Client_Company");
            fsql.Ado.ExecuteNonQuery("truncate table Project_ContactsInfo");
            fsql.Ado.ExecuteNonQuery("truncate table ProjectBasics");
            fsql.Ado.ExecuteNonQuery("truncate table ProjectDynamic");


            int lastProjectId = 0;

            foreach (DataRow row in table.Rows)
            {
                //1 客户业主
                var customer = Convert.ToString(row["客户"]);
                var owner = Convert.ToString(row["业主"]);
                var customerId = 0;
                var ownerId = 0;

                Debug.WriteLine($"插入客户信息[{customer}],[{owner}]");

                if (!string.IsNullOrEmpty(customer))
                {
                    var client = clientRepo.Select.Where(x => x.ClientName == customer && !x.IsDeleted).ToOne();
                    if (client == null)
                        client = clientRepo.Insert(new Client_CompanyEntity { ClientName = customer });
                    customerId = client.Id;
                }
                if (!string.IsNullOrEmpty(owner))
                {
                    var client = clientRepo.Select.Where(x => x.ClientName == owner && !x.IsDeleted).ToOne();
                    if (client == null)
                        client = clientRepo.Insert(new Client_CompanyEntity { ClientName = owner });
                    ownerId = client.Id;
                }

                //2 添加项目信息
                var date = DateTime.MinValue;
                if (!DateTime.TryParse(Convert.ToString(row["年份"]), out date))
                {
                    int year = 0;
                    if (int.TryParse(Convert.ToString(row["年份"]), out year))
                        date = new DateTime(year, 1, 1);
                }

                var business = Convert.ToString(row["业务板块"]);
                int businessId = 0;
                if (businessDict.ContainsKey(business))
                    businessId = businessDict[business];
                var projectCode = Convert.ToString(row["项目号"]);
                var projectName = Convert.ToString(row["项目名称"]);
                var status = Convert.ToString(row["设备状态"]);
                int statusID = 0;
                if (!string.IsNullOrEmpty(status))
                    statusID = statusDict[status];
                Debug.WriteLine($"插入项目信息{projectCode}-{projectName}");
                int parentId = 0;
                if (string.IsNullOrEmpty(projectCode))
                    parentId = lastProjectId;
                projRepo.Insert(new ProjectBasicsEntity
                {
                    ProjectCode = projectCode,
                    ProjectName = projectName,
                    ClientCompany = customerId,
                    OwnerUnitID = ownerId,
                    //项目状态
                    ProjectStatus = statusID,
                    BusinessType = businessId,
                    ParentId = parentId,
                    //项目阶段
                    ProState = 0,
                    ProjectReceiveDate = date,
                });

                var projectId = Convert.ToInt32(fsql.Ado.ExecuteScalar("select @@identity;"));
                if (!string.IsNullOrEmpty(projectCode))
                    lastProjectId = projectId;

                //3 成员字典 + 项目成员

                var members = new Dictionary<string, string>();
                for (int i = 0; i < fzbkNames.Length; i++)
                {
                    var name = fzbkNames[i];
                    var value = Convert.ToString(row[name]);
                    if (string.IsNullOrEmpty(value)) continue;
                    else if (value == "/" || value == "\\") continue;
                    else if (value == "委外") continue;

                    members[value] = name;
                }

                var memberList = members.Select(x => x.Key).ToList();

                var users = userRepo.Select.From<OrganizationEntity, PostEntity>((join, org, post) =>
                    join.LeftJoin(o => o.OrgId == org.Id).LeftJoin(p => p.PostId == post.Id)
                ).Where(x => memberList.Contains(x.t1.RealName)).ToDictionary(x => x.RealName, x => x);
                var exts = userExtRepo
                    .Where(x => userRepo.Where(x => memberList.Contains(x.RealName) && !x.IsDeleted)
                    .ToList(x => x.Id).Contains(x.UserId)).ToDictionary(x => x.UserId, x => x);
                Debug.WriteLine("插入联系人信息");
                foreach (var item in members)
                {
                    if (!users.ContainsKey(item.Key)) continue;
                    if (!exts.ContainsKey(users[item.Key].Id)) continue;
                    var user = users[item.Key];
                    var ext = exts[user.Id];

                    contactRepo.Insert(new Project_ContactsInfo
                    {
                        ContactsID = user.Id,
                        Dept = user.OrgId,
                        PID = projectId,
                        FZBKId = fzbkDict[members[user.RealName]],
                    });
                }



                //4 添加项目动态
                var logs = Convert.ToString(row["设计状态"]);
                if (!string.IsNullOrEmpty(logs))
                    logsRepo.Insert(new ProjectDynamicEntity
                    {
                        projectID = projectId,
                        operationContent = logs
                    });
            }
        }

        public Dictionary<string, int> GetDictionary(IBaseRepository<DictionaryEntity> fsql, string pname, string[] childs)
        {
            var entity = fsql.Where(x => x.DictionaryText == pname && !x.IsDeleted).ToOne();
            if (entity == null)
                entity = fsql.Insert(new DictionaryEntity { DictionaryText = pname });
            var notExists = childs.Where(x => !fsql.Select.Any(a => a.DictionaryText == x && !a.IsDeleted)).ToList();
            foreach (var item in notExists)
            {
                fsql.Insert(new DictionaryEntity { ParentId = entity.Id, DictionaryText = item, DictionaryValue = item });
            }

            var dict = fsql.Where(x => x.ParentId == entity.Id && !x.IsDeleted).ToDictionary(x => x.DictionaryText, x => x.Id);
            return dict;
        }
    }
}
