<template>
  <training-list></training-list>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import TrainingList  from "@/components/TrainingList.vue";

@Component({
    components:{
        TrainingList
    }
})
export default class TrainingsList extends Vue {}
</script>
