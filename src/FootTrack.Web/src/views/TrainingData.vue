<template>
  <training-details></training-details>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import TrainingDetails from "@/components/TrainingDetails.vue";

@Component({
    components:{
        TrainingDetails
    }
})
export default class TrainingData extends Vue {}
</script>
