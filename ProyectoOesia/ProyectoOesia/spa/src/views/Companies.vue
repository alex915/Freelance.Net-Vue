

<template>
  <section>
    <v-container>
    <v-row>
      <v-col
        v-for="company in companies"
        :key="company.id"
      >
        <company-card :company="company"></company-card>
      </v-col>
    </v-row>
  </v-container>
    
  </section>
</template>

<script lang="ts">
import CompanyCard from "@/components/CompanyCard.vue";
import { Component, Vue, Watch } from "vue-property-decorator";
import { companiesService } from "@/services/companies.service";
import { AxiosResponse } from "axios";

@Component({
  components: {
    'company-card': CompanyCard,
  }
})
export default class Admin extends Vue {
  public companies: any[] = [];

  public mounted(): void {
    this.getCompanies();
  }

  public getCompanies() {
    this.$spinner.showSpinner();
    companiesService
      .getCompanies()
      .then((response: AxiosResponse) => {
        console.log(response.data);
        
        this.companies = response.data.$values.filter((x: any) => !!x.$id);
      })
      .finally(this.$spinner.removeSpinner());
  }
}
</script>

<style>
</style>