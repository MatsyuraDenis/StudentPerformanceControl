import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import ContentPage from "../views/contentPage.vue";
import Groups from "../views/groups.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Home",
    component: ContentPage
  },
  {
    path: '/groups',
    name: "GROUPS",
    component: Groups
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
