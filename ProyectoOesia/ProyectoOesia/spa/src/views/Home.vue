<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    {{ token }}
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({
  components: {
  },
})
export default class Home extends Vue {
  public get token(): string {
    return this.$store.getters['getToken'];
  }
}
</script>
