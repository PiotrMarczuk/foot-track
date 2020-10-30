<template>
  <v-app>
    <BottomNavigation/>
    <Navbar/>
    <alert v-if="alert" :msg="alert.message" color="accent"></alert>
    <v-main>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import Navbar from "@/components/Navbar.vue";
import Alert from "@/components/Alert.vue";
import Component from "vue-class-component";
import BottomNavigation from "@/components/BottomNavigation.vue";

@Component({
  components: {
    Navbar,
    Alert,
    BottomNavigation
  }
})
export default class extends Vue {
  get alert() {
    return this.$store.state.alert.message;
  }
}
</script>
