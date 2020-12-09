

<template>
  <div>
    <v-data-table
      :headers="headers"
      :items="users"
      sort-by="email"
      class="elevation-1"
      mobile-breakpoint="850"
      :calculate-widths="true"
      item-key="identifier"
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Usuarios</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-dialog v-model="dialogDelete" max-width="500px">
            <v-card>
              <v-card-title class="headline"
                >Estás seguro que quieres borrar este usuario?</v-card-title
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
      <template v-slot:item.emailConfirmed="{ item }">
        <v-chip
          v-if="item.emailConfirmed"
          class="ma-2"
          color="green"
          text-color="white"
          small
        >
          Si
        </v-chip>
        <v-chip
          v-if="!item.emailConfirmed"
          class="ma-2"
          color="red"
          text-color="white"
          small
        >
          No
        </v-chip>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
      </template>
      <template v-slot:no-data> No hay datos </template>
    </v-data-table>
  </div>
</template>

<script lang="ts">
import { usersService } from "@/services/users.service";
import { Component, Vue, Watch } from "vue-property-decorator";

@Component
export default class Users extends Vue {
  public dialog = false;
  public dialogDelete = false;
  public headers = [
    { text: "Nombre", value: "firstName" },
    { text: "Apellido", value: "lastName" },
    { text: "Email", value: "email" },
    { text: "Telefono", value: "phoneNumber" },
    { text: "Email confirmado?", value: "emailConfirmed" },
    { text: "", value: "actions", sortable: false },
  ];

  public users: any[] = [];
  public deletedIndex = -1;
  public deletedItem = {};
  public defaultItem = {};

  @Watch("dialogDelete")
  public dialogDeleteWatch(val: any) {
    val || this.closeDelete();
  }

  public created() {
    this.initialize();
  }

  public initialize() {
    this.$spinner.showSpinner();
    usersService
      .get()
      .then((response: any) => {
        this.users = response.data.$values;
      })
      .finally(this.$spinner.removeSpinner());
  }

  public deleteItem(item: any) {
    this.deletedIndex = this.users.indexOf(item);
    this.deletedItem = Object.assign({}, item);
    this.dialogDelete = true;
  }

  public deleteItemConfirm() {
    this.$spinner.showSpinner();
    const user = this.users[this.deletedIndex];
    usersService
      .delete(user.id)
      .then((response: any) => {
        this.users.splice(this.deletedIndex, 1);
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