<template>
  <div>
    <create-company />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { authService } from "@/services/auth.service";
import CreateCompany from "@/components/CreateCompany.vue";

@Component({
  components: {
    "create-company": CreateCompany,
  },
})
export default class NewCompany extends Vue {
  public get hasCompany(): boolean {
    return this.$store.getters["user"]?.company?.id != -1;
  }

  public created() {
    if (this.hasCompany) {
      this.$router.push({ name: "Home" });
    }
  }
}
</script>

<style>
</style>