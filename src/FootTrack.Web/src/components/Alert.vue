<template>
  <v-snackbar :value="true" top>{{ msg }}</v-snackbar>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";

@Component
export default class Alert extends Vue {
  @Prop() private msg!: string;
}
</script>
