<template>
  <div id="content-screen">
    <router-view></router-view>
  </div>
</template>

<script>
import { paths } from "../entities/constants/paths";

export default {
  name: "contentPage",
  mounted: function() {
    this.$router.push(paths.GROUPS);
  }
};
</script>

<style scoped></style>
