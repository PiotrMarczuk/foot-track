import store from "@/store";
import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Home from "../views/Home.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/training",
    name: "Training",
    component: () =>
      import("@/views/Training.vue")
  },
  {
    path: "/about",
    name: "About",
    component: () =>
      import("@/views/About.vue")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to, from, next) => {
  const publicPages = ["/"];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem("user");
  const { getters } = store;
  if (authRequired && !loggedIn && getters["form/isRegisterFormVisible"]) {
    const { commit } = store;
    commit("form/setLoginFormVisible", true);
    return;
  }

  next();
});
export default router;
