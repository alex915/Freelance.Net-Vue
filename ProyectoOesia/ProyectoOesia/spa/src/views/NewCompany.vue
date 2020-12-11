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
    <create-company class="mitop" />
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

<style scoped>
.mitop{
  margin-top: 180px;
}
</style>