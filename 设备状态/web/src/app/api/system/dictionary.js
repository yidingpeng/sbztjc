import {
	http
} from '@/utils/http.js'

export function getAll(params) {
	return http.request({
		url: '/dictionary/all',
		method: 'get',
		params,
	})
}

//JSON,数组根据字段分组
export function GroupbyName(data, Name) { //data数据源，Name 根据什么字段分组
	var map = {},
		dest = [];
	for (var i = 0; i < data.length; i++) {
		var ai = data[i];
		if (!map[ai[Name]]) {
			dest.push({
				name: ai[Name],
				data: [ai]
			});
			map[ai[Name]] = ai;
		} else {
			for (var j = 0; j < dest.length; j++) {
				var dj = dest[j];
				if (dj.name == ai[Name]) {
					dj.data.push(ai);
					break;
				}
			}
		}
	}
	return dest;
}
