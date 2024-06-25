const path = require('path');

module.exports = {
  chainWebpack: config => {
    config.resolve.alias
      .set('@', path.resolve(__dirname, 'src'))
      .set('components', path.resolve(__dirname, 'src/components'))
      .set('views', path.resolve(__dirname, 'src/views'));
  },
  outputDir: path.resolve(__dirname, '../EportfolioRestAPI/EportfolioRestAPI/wwwroot'), 
};
