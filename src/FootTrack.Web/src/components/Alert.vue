 <template>
  <vs-alert solid>
    <template #title>
      Vuesax Framework
    </template>
    {{ msg }}
  </vs-alert>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";

@Component
export default class Alert extends Vue {
  @Prop() private msg!: string;
}
</script>