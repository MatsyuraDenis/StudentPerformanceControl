<template>
  <div id="app">
    <nav-bar></nav-bar>
    <content-page></content-page>
  </div>
</template>

<script>
import navBar from "@/views/navBar";
import contentPage from "@/views/contentPage";

export default {
  name: "app",
  components: {
    navBar,
    contentPage
  }
};
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
