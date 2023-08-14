const list = [{ bomCode: '123', bomName: 'test' }]

module.exports = [
  {
    url: '/bom',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { list },
      }
    },
  },
]
