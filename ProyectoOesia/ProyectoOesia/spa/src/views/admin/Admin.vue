<template>
  <div>
    <v-data-table
      :headers="headers"
      :items="companies"
      sort-by="identifier"
      class="elevation-1"
      mobile-breakpoint="850"
      :calculate-widths="true"
      :single-expand="true"
      :expanded.sync="expanded"
      item-key="id"
      show-expand
    >
      <template v-slot:expanded-item="{item }">
        <td :colspan="6" style="background-color:#BBDEFB">
            Dirección: {{ item.address }}
            {{ item.pc }}
            {{ item.city }}
            {{ item.province }}
            {{ item.country }}
        </td>
      </template>
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Compañias</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-dialog v-model="dialogDelete" max-width="500px">
            <v-card>
              <v-card-title class="headline"
                >Estás seguro que quieres borrar esta compañia?</v-card-title
              >
              <v-spacer></v-spacer>
              <v-card-subtitle>Este proceso es irreversible.</v-card-subtitle>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="closeDelete"
                  >Cancelar</v-btn
                >
                <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                  >OK</v-btn
                >
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
      </template>
      <template v-slot:no-data> No hay datos </template>
    </v-data-table>
  </div>
</template>

<script lang="ts">
import { authService } from "@/services/auth.service";
import { companiesService } from "@/services/companies.service";
import { Component, Vue, Watch } from "vue-property-decorator";

@Component
export default class Admin extends Vue {
  public dialog = false;
  public dialogDelete = false;
  public headers = [
    { text: "Identificador", value: "id" },
    { text: "Compañia", value: "companyName" },
    { text: "Email", value: "email" },
    { text: "Telefono", value: "phone" },
    { text: "", value: "actions", sortable: false },
  ];

  public companies: any[] = [];
  public deletedIndex = -1;
  public deletedItem = {};
  public defaultItem = {};

  @Watch("dialogDelete")
  public dialogDeleteWatch(val: any) {
    val || this.closeDelete();
  }

  public created() {
    this.$spinner.showSpinner();
    authService
      .getUser()
      .then((x) => {
        this.$store.dispatch("setUser", x.data);
      })
      .finally(this.$spinner.removeSpinner());
    this.initialize();
  }

  public initialize() {
    this.$spinner.showSpinner();
    companiesService
      .getCompanies()
      .then((response: any) => {
        console.log(response.data.$values);
        this.companies = response.data.$values;
      })
      .finally(this.$spinner.removeSpinner());
  }

  public deleteItem(item: any) {
    this.deletedIndex = this.companies.indexOf(item);
    this.deletedItem = Object.assign({}, item);
    this.dialogDelete = true;
  }

  public deleteItemConfirm() {
    this.$spinner.showSpinner();
    const company = this.companies[this.deletedIndex];
    companiesService
      .deleteCompany(company.id)
      .then((response: any) => {
        this.companies.splice(this.deletedIndex, 1);
      })
      .finally(this.$spinner.removeSpinner());
    this.closeDelete();
  }

  public closeDelete() {
    this.dialogDelete = false;
    this.$nextTick(() => {
      this.deletedItem = Object.assign({}, this.defaultItem);
      this.deletedIndex = -1;
    });
  }
}
</script>

<style>
</style>