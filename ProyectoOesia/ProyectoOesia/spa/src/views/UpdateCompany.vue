<template>
  <div>
     <v-app-bar
     fixed
      color="#359ff4"
      dark
      dense
      class="mleft expand"
      scroll-threshold="60"
      :floating="false"
    >
      <v-spacer></v-spacer>
      
      <v-toolbar-title>Freelance.Net</v-toolbar-title>
      <v-spacer></v-spacer>


    </v-app-bar>
    <update-company class="mitop" />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import UpdateCompanyComponent from "@/components/UpdateCompanyComponent.vue";

@Component({
  components: {
    "update-company": UpdateCompanyComponent,
  },
})
export default class UpdateCompany extends Vue {
  public get hasCompany(): boolean {
    return this.$store.getters["user"]?.company?.id != -1;
  }

  public created() {
    if (!this.hasCompany) {
      this.$router.push({ name: "New Company" });
    }
  }
}
</script>

<style scoped>
.mitop{
  margin-top: 100px;
}

</style>