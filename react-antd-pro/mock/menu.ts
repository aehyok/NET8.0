export default {
  'GET /api/getMenuList': {
    code: 200,
    message: 'success',
    data: [
      {
        action: 0,
        biggest: 128,
        children: [
          {
            action: 0,
            biggest: 167,
            children: [],
            code: '两委会议待评议',
            description: '',
            id: 193,
            leaf: 1,
            name: '两委会议待评议',
            pcode: '村两委民主评议',
            sequence: 1,
            status: 1,
            uiPath: '/dvs-ffp/beReviewed',
          },
          {
            action: 0,
            biggest: 173,
            children: [],
            code: '两委会议评议通过',
            description: '',
            id: 195,
            leaf: 1,
            name: '两委会议评议通过',
            pcode: '村两委民主评议',
            sequence: 2,
            status: 1,
            uiPath: '/dvs-ffp/approvalIndex',
          },
          {
            action: 0,
            biggest: 172,
            children: [],
            code: '两委会议评议不通过',
            description: '',
            id: 197,
            leaf: 1,
            name: '两委会议评议不通过',
            pcode: '村两委民主评议',
            sequence: 3,
            status: 1,
            uiPath: '/dvs-ffp/failIndex',
          },
          {
            action: 0,
            biggest: 169,
            children: [],
            code: '评议公示管理',
            description: '',
            id: 199,
            leaf: 1,
            name: '评议公示管理',
            pcode: '村两委民主评议',
            sequence: 4,
            status: 1,
            uiPath: '/dvs-ffp/commentIndex',
          },
        ],
        code: '村两委民主评议',
        description: '',
        id: 191,
        leaf: 1,
        name: '村两委民主评议',
        pcode: 'dvs-ffp',
        sequence: 1,
        status: 1,
        uiPath: '#',
      },
      {
        action: 0,
        biggest: 128,
        children: [
          {
            action: 0,
            biggest: 173,
            children: [],
            code: '公示名单管理',
            description: '',
            id: 203,
            leaf: 1,
            name: '公示名单管理',
            pcode: '评议公示名单管理',
            sequence: 1,
            status: 1,
            uiPath: '/dvs-ffp/publicityListIndex',
          },
          {
            action: 0,
            biggest: 169,
            children: [],
            code: '评议报告管理',
            description: '',
            id: 205,
            leaf: 1,
            name: '评议报告管理',
            pcode: '评议公示名单管理',
            sequence: 2,
            status: 1,
            uiPath: '/dvs-ffp/commentReportIndex',
          },
        ],
        code: '评议公示名单管理',
        description: '',
        id: 201,
        leaf: 1,
        name: '评议公示名单管理',
        pcode: 'dvs-ffp',
        sequence: 2,
        status: 1,
        uiPath: '#',
      },
      {
        action: 0,
        biggest: 168,
        children: [],
        code: '花名册管理',
        description: '',
        id: 207,
        leaf: 1,
        name: '花名册管理',
        pcode: 'dvs-ffp',
        sequence: 3,
        status: 1,
        uiPath: '/dvs-ffp/roster-management',
      },
      {
        action: 0,
        biggest: 167,
        children: [],
        code: '区域网格管理',
        description: '',
        id: 209,
        leaf: 1,
        name: '区域网格管理',
        pcode: 'dvs-ffp',
        sequence: 4,
        status: 1,
        uiPath: '/dvs-ffp/grid-management',
      },
      {
        action: 0,
        biggest: 164,
        children: [],
        code: '防返贫字典',
        description: '',
        id: 211,
        leaf: 1,
        name: '防返贫字典',
        pcode: 'dvs-ffp',
        sequence: 5,
        status: 1,
        uiPath: '/dvs-ffp/prevent-dictionaries',
      },
    ],
  },
  'GET /api/getMenu': {
    code: 200,
    message: 'success',
    data: {
      action: 0,
      biggest: 128,
      code: '村两委民主评议',
      description: '',
      id: 191,
      leaf: 1,
      name: '村两委民主评议',
      pcode: 'dvs-ffp',
      sequence: 1,
      status: 1,
      uiPath: '#',
    },
  },
};