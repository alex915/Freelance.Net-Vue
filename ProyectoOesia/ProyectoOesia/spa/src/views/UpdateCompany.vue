<template>
  <div>
    <update-company />
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

<style>
</style>