import Vue from 'vue'
import App from './App.vue'
import {routes} from "./route/routes";

Vue.config.productionTip = false
Vue.config.productionTip = false;

window.axios = require('axios');

new Vue({
  render: h => h(App),
}).$mount('#app')


