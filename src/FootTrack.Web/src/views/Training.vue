<template>
<google-heatmap></google-heatmap>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import GoogleHeatmap from "@/components/GoogleHeatmap.vue";

@Component({
    components:{
        GoogleHeatmap
    }
})
export default class Training extends Vue {
}
</script>
