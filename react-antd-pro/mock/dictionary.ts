// 代码中会兼容本地 service mock 以及部署站点的静态数据
export default {
  // GET 获取字典类型列表
  'GET /api/getDictionaryTypeList': {
    code: 200,
    message: 'success',
    data: [
      {
        name: '与户主关系',
        typeCode: 1010,
      },
      {
        name: '主要生活来源',
        typeCode: 1011,
      },
      {
        name: '人口属性',
        typeCode: 1012,
      },
      {
        name: '民族',
        typeCode: 1013,
      },
      {
        name: '婚姻状况',
        typeCode: 1014,
      },
      {
        name: '政治面貌',
        typeCode: 1015,
      },
      {
        name: '学历',
        typeCode: 1016,
      },
      {
        name: '宗教信仰',
        typeCode: 1017,
      },
      {
        name: '门牌标签',
        typeCode: 2010,
      },
      {
        name: '地块类型',
        typeCode: 3010,
      },
      {
        name: '规划类型',
        typeCode: 3011,
      },
      {
        name: '所属行业',
        typeCode: 4010,
      },
      {
        name: '工种',
        typeCode: 4011,
      },
      {
        name: '薪资范围',
        typeCode: 4012,
      },
      {
        name: '经济收入来源',
        typeCode: 5010,
      },
      {
        name: '异常情况',
        typeCode: 6010,
      },
      {
        name: '疫区接触情况',
        typeCode: 6011,
      },
      {
        name: '耕地种植类型',
        typeCode: 7010,
      },
      {
        name: '大棚类型',
        typeCode: 7011,
      },
      {
        name: '企业类型',
        typeCode: 7012,
      },
      {
        name: '设施类型',
        typeCode: 7013,
      },
      {
        name: '事件类型',
        typeCode: 8010,
      },
    ],
  },

  'GET /api/getDictionaryList': {
    code: 200,
    message: 'success',
    page: 1,
    pages: 2,
    total: 12,
    limit: 10,
    data: [
      {
        code: 1010100,
        description: '',
        fontColor: '#E62222',
        iconFileId: 9865,
        iconFileUrl:
          'https://dvs-dev.sunlight-tech.com/sundfs/f/2/f229bee1a509f170bace4c5b30b4fbdc.png',
        id: 121,
        name: '本人',
        remark: '1111',
        sequence: 10,
        status: 1,
        typeCode: 1010,
      },
      {
        code: 1010101,
        description: '222',
        fontColor: '#000DFF',
        iconFileId: 588,
        iconFileUrl:
          'http://dvs-dev.sunlight-tech.com/sundfs/b/8/b87b9681d87ce35f26a8f1885ef0d3d0.jpg',
        id: 37,
        name: '配偶',
        remark: '',
        sequence: 20,
        status: 1,
        typeCode: 1010,
      },
      {
        code: 1010102,
        description: '',
        fontColor: '',
        iconFileId: 0,
        iconFileUrl: '',
        id: 38,
        name: '子',
        remark: '',
        sequence: 30,
        status: 1,
        typeCode: 1010,
      },
      {
        code: 1010103,
        description: '',
        fontColor: '',
        iconFileId: 0,
        iconFileUrl: '',
        id: 67,
        name: '女',
        remark: '',
        sequence: 40,
        status: 1,
        typeCode: 1010,
      },
      {
        code: 1010104,
        description: '',
        fontColor: '',
        iconFileId: 0,
        iconFileUrl: '',
        id: 68,
        name: '孙子、孙女',
        remark: '',
        sequence: 50,
        status: 1,
        typeCode: 1010,
      },
      {
        code: 1010105,
        description: '',
        fontColor: '',
        iconFileId: 0,
        iconFileUrl: '',
        id: 69,
        name: '外孙子、外孙女',
        remark: '',
        sequence: 60,
        status: 1,
        typeCode: 1010,
      },
      {
        code: 1010106,
        description: '',
        fontColor: '',
        iconFileId: 0,
        iconFileUrl: '',
        id: 70,
        name: '父母',
        remark: '',
        sequence: 70,
        status: 1,
        typeCode: 1010,
      },
      {
        code: 1010107,
        description: '',
        fontColor: '',
        iconFileId: 0,
        iconFileUrl: '',
        id: 71,
        name: '祖父母或外祖父母',
        remark: '',
        sequence: 80,
        status: 1,
        typeCode: 1010,
      },
      {
        code: 1010108,
        description: '',
        fontColor: '#E01212',
        iconFileId: 0,
        iconFileUrl: '',
        id: 72,
        name: '兄、弟、姐、妹',
        remark: '',
        sequence: 90,
        status: 1,
        typeCode: 1010,
      },
      {
        code: 1010109,
        description: '',
        fontColor: '',
        iconFileId: 0,
        iconFileUrl: '',
        id: 73,
        name: '儿媳',
        remark: '',
        sequence: 100,
        status: 1,
        typeCode: 1010,
      },
    ],
  },
};