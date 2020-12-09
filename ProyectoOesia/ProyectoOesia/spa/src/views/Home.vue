<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />
    <div v-if="token">
      {{ token }}
    </div>
    <div v-if="user && user.companyId != null">
      {{ user.company }}
    </div>

    <appsearch/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { authService } from "@/services/auth.service";
import Search from "@/components/Search.vue";


@Component({
  components: {
    "appsearch":Search
  },
})
export default class Home extends Vue {
  public user = null;

  public get token(): string {
    return this.$store.getters["getToken"];
  }
  public mounted() {
    
    if (this.token) {
      this.$spinner.showSpinner();
      authService.getUser().then((x) => {
        this.$spinner.removeSpinner();
        this.user = x.data;
        this.$store.dispatch("setUser", this.user);
        console.log(this.user);
        if (x.data.company.id == -1) {
          this.$router.push({ name: "New Company" });
        }
      });
    }
  }
}
</script>

<style>
</style>