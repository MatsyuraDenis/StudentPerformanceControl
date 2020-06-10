<template>
  <div>
    Group list
    <div v-for="(group, index) in getGroups()" :key="index">{{group.id}}</div>
    <div>{{show()}}</div>
  </div>
</template>

<script>
  import axios from 'axios';
  import {endpoints} from "@/router/endpoints";

  export default {
    name: "groups",
    data: function() {
      return{
        groups: ''
      }
    },
    methods: {
      async getGroups() {
        const response = await axios.get(endpoints.getGroupsInfo);
        console.log(response.data);
        return response.data;
      },
      show() {
        console.log('s');
      }
    },
    async mounted() {
      const response = await axios.get(endpoints.getGroupsInfo);
      console.log(response.data);
      this.groups = response.data;
    }
  }
</script>

<style scoped>

</style>