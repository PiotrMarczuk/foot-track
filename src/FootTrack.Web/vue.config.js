module.exports = {
  css: {
    loaderOptions: {
      scss: {
        prependData: '@import "@/common/styles.scss";'
      }
    }
  },
  transpileDependencies: ["vuetify"]
};
