<template>
  <v-card elevation="24" class="mx-auto">
    <v-carousel
      :continuous="true"
      :interval="10000"
      :cycle="cycle"
      :show-arrows="false"
      hide-delimiter-background
      delimiter-icon="mdi-minus"
      @change="change"
    >
      <v-carousel-item v-for="(quote, i) in quotes" :key="i">
        <v-sheet color="secondary" height="100%" tile>
          <v-row class="fill-height" align="center" justify="center">
            <span class="headline text-center quote">{{ quote }}</span>
          </v-row>
        </v-sheet>
      </v-carousel-item>
    </v-carousel>
    <v-list two-line>
      <v-list-item>
        <v-list-item-avatar>
          <v-img :src="currentAuthorPhoto"></v-img>
        </v-list-item-avatar>
        <v-list-item-content>
          <v-list-item-title>{{ currentAuthor }}</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list>
  </v-card>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class QuotesCarousel extends Vue {
  cycle = true;
  quotes = [
    "“It's like you wanting to marry Miss World and she doesn't want you. I can try to help you but if she does not want to marry you what can I do?”",
    "“Sometimes you see beautiful people with no brains. Sometimes you have ugly people who are intelligent, like scientists.”",
    "“When an Italian says it’s pasta I check under the sauce to make sure. They are innovators of the smokescreen.”",
    "“Crazy players love me – I don’t know why.”",
    "“Everything is going right for Poland. Well, almost everything.”"
  ];
  authors = [
    "Arsene Wenger",
    "Jose Mourinho",
    "Sir Alex Ferguson",
    "Jurgen Klopp",
    "Franciszek Smuda"
  ];
  images = [
    "https://i.iplsc.com/arsene-wenger/0009DPZJTTOXPQJH-C122-F4.jpg",
    "https://i.insider.com/5de78297fd9db262650467b2?width=1136&format=jpeg",
    "https://gfx.sport.radiozet.pl/var/radiozet-sport/storage/images/pilka-nozna/sir-alex-ferguson-opuscil-szpital-i-zacznie-wracac-do-zdrowia-w-swoim-domu/224291-1-pol-PL/Sir-Alex-Ferguson-opuscil-szpital.-Chce-wrocic-na-Old-Trafford_article.jpg",
    "https://bi.im-g.pl/im/cc/98/18/z25791180IER,Jurgen-Klopp.jpg",
    "https://bi.im-g.pl/im/97/41/16/z23336343IER,Franciszek-Smuda.jpg"
  ];

  currentIndex = 0;

  get currentAuthor() {
    return this.authors[this.currentIndex];
  }

  get currentAuthorPhoto() {
    return this.images[this.currentIndex];
  }

  change(number: any) {
    this.currentIndex = number;
  }
}
</script>

<style lang="scss" scoped>
.quote{
  padding: 100px;
}
</style>