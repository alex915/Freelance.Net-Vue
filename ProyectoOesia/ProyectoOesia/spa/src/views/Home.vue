<template>
  <div class="home">
    
      <update-company />
      <create-company />
    
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { authService } from "@/services/auth.service";
import CreateCompany from "@/components/CreateCompany.vue";
import UpdateCompany from "@/components/UpdateCompany.vue";
import StaticMap from "@/components/StaticMap.vue";

@Component({
  components: {
    "create-company": CreateCompany,
    "update-company": UpdateCompany,
    "maps": StaticMap,
  },
})
export default class Home extends Vue {
  public user: any = {};
  public lat = "";
  public lon = "";
  public km = "";
  public ticks = [
    "0km",
    "1km",
    "25km",
    "50km",
    "75km",
    "100km",
    "125km",
    "150km",
    "175km",
    "200km",
  ];
  public get token(): string {
    return this.$store.getters["token"];
  }

  public get hasCompany(): boolean {
    return this.user?.company?.id != -1;
  }

  public mounted() {
    if (this.token) {
      this.$spinner.showSpinner();
      authService.getUser().then((x) => {
        this.$spinner.removeSpinner();
        this.user = x.data;
        this.$store.dispatch("setUser", this.user);
        console.log(this.user);

        if (x.data.company.id != -1) {
          this.lat = this.user.company.latitude;
          this.lon = this.user.company.longitude;
          this.km = this.user.company.maxDistance;
          // this.$router.push({ name: "New Company" });
        }
      });
    }
  }
}
</script>

<style>
.home {
  margin-left: 56px;
}
</style>