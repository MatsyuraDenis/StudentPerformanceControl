<template>
  <div id="app">
    <student-performance></student-performance>
  </div>
</template>

<script>
import studentPerformance from "./components/contentPages/studentPerformance";
  // eslint-disable-next-line no-unused-vars
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'

export default {
  name: 'app',
  components: {
    studentPerformance
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
