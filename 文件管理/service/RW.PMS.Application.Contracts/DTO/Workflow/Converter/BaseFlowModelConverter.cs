using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow.Converter
{
    /// <summary>
    /// JSON序列化BaseFlowModel基类时的转换，需要将基类根据Type属性转换成特定子类。
    /// </summary>
    public class BaseFlowModelConverter : JsonConverter<BaseFlowModel>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert == typeof(BaseFlowModel)) return true;
            var result = base.CanConvert(typeToConvert);
            return result;
        }

        public override BaseFlowModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            BaseFlowModel models = null;
            var node = JsonNode.Parse(ref reader);
            var parent = node.AsObject();
            parent.TryGetPropertyValue("type", out JsonNode parentTypeNode);
            if (parentTypeNode == null) return null;
            var parentType = parentTypeNode.GetValue<int>();
            if (parentType == 0)
                models = JsonSerializer.Deserialize<Promoter>(parent, options);
            else if (parentType == 1)
                models = JsonSerializer.Deserialize<ApprovideNode>(parent, options);
            else if (parentType == 4)
                models = JsonSerializer.Deserialize<ConditionNode>(parent, options);
            else if (parentType == 3)
                models = JsonSerializer.Deserialize<ConditionItemNode>(parent, options);
            else if (parentType == 2)
                models = JsonSerializer.Deserialize<SendNode>(parent, options);

            return models;
        }

        public override void Write(Utf8JsonWriter writer, BaseFlowModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
            //throw new NotImplementedException();
            //writer.WriteStringValue()
            //string str = JsonSerializer.Serialize(value, options);
            //writer.WriteString(JsonEncodedText.Encode("utf8"), str);
        }
    }
}
