const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function(app) {
  app.use(
    '/api',
    createProxyMiddleware({
      target: 'https://books-cecygvckhaceh3ax.eastus-01.azurewebsites.net', // Backend URL
      changeOrigin: true,
      secure: false
    })
  );
};