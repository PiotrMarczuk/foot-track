<template>
<div class="mx-auto">
  <v-card color="primary" dark>
    <v-card-title>
      <v-icon large left>
        mdi-twitter
      </v-icon>
      <span class="title font-weight-light">Twitter</span>
    </v-card-title>

    <v-card-text class="headline font-weight-bold">
      Little birdy told me that when you login to Foot Track you can get access
      to all the best features.
    </v-card-text>

    <v-card-actions class="end">
      <v-list-item class="grow">
        <v-list-item-avatar color="grey darken-3">
          <v-img
            class="elevation-6"
            src="https://avataaars.io/?avatarStyle=Transparent&topType=ShortHairShortFlat&accessoriesType=Sunglasses&hairColor=Brown&facialHairType=Blank&clotheType=Hoodie&clotheColor=Black&eyeType=Default&eyebrowType=Default&mouthType=Default&skinColor=Light"
          ></v-img>
        </v-list-item-avatar>

        <v-list-item-content>
          <v-list-item-title>Piotr Marczuk</v-list-item-title>
        </v-list-item-content>

        <v-row align="center" justify="end">
          <v-btn rounded color="secondary" @click="loginClick">Sign in</v-btn>
        </v-row>
      </v-list-item>
    </v-card-actions>
  </v-card>
</div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Mutation } from "vuex-class";

@Component
export default class SignupCard extends Vue {
  @Mutation("form/setLoginFormVisible") setLoginFormVisibility: any;

  loginClick() {
    this.setLoginFormVisibility(true);
  }
}
</script>