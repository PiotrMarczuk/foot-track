<template>
  <v-app>
    <Navbar/>
    <alert v-if="alert" :msg="alert.message" color="accent"></alert>
    <v-main>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </v-main>
    <Footer/>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import Navbar from "@/components/Navbar.vue";
import Footer from "@/components/Footer.vue";
import Alert from "@/components/Alert.vue";
import Component from 'vue-class-component';

@Component({
  components: {
    Navbar,
    Footer,
    Alert
  }
})
export default class extends Vue{
  get alert(){
    console.log(this.$store.state.alert.message);
    return this.$store.state.alert.message;
  }
}
</script>
