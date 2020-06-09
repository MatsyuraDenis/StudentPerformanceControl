<template>
  <div class="hello">
    {{info}}
  </div>
</template>

<script>

import {api} from "../route/api";

export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  mounted: function() {
    // eslint-disable-next-line no-console
    console.log(api.getStudentPerformance(1));
    this.info = api.getStudentPerformance(1);
  },
  data: function () {
    return{
      info: null
    }
  },
  methods: {

  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
