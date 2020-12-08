<template>
  <div class="updateUser">
    <div class="shadow">
      <v-row justify="center">
        <v-col class="padding" cols="11" sm="12" md="12" lg="8">
          <v-form
            action
            class="form"
            v-model="isValid"
            @submit.prevent="update"
          >
            <v-row justify="center" class="pb-6">
              <v-col cols="12">
                <h4>Actualiza tus datos</h4>
              </v-col>
            </v-row>
            <v-row justify="center">
              <v-col cols="12" sm="9" md="6" lg="6">
                <v-text-field
                  v-model="firstName"
                  :outlined="true"
                  label="Nombre"
                  :rules="[requiredRules]"
                  hint="Introduzca su nombre"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="9" md="6" lg="6">
                <v-text-field
                  v-model="lastName"
                  :rules="[requiredRules]"
                  :outlined="true"
                  label="Apellido"
                  hint="Introduzca su apellido"
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row justify-md="space-between" justify="center">
              <v-col cols="12" sm="9" md="7" lg="7">
                <v-text-field
                  v-model="email"
                  :outlined="true"
                  :rules="[requiredRules, emailRules]"
                  label="Email"
                  hint="Introduzca su correo electrónico"
                  autocomplete="username"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="9" md="4" lg="4">
                <div class="flex">
                  <v-text-field
                    v-model="ext"
                    :outlined="true"
                    label="Codigo"
                    prefix="+"
                    class="widthExt"
                  ></v-text-field>
                  <v-text-field
                    v-model="phone"
                    :outlined="true"
                    label="Telefono"
                  ></v-text-field>
                </div>
              </v-col>
            </v-row>
            <v-row justify="center">
              <v-col cols="12" sm="9" md="6" lg="6">
                <v-text-field
                  v-model="newPassword"
                  type="password"
                  :rules="[requiredRules]"
                  :outlined="true"
                  label="Nueva contraseña"
                  hint="Introduzca una nueva contraseña"
                  autocomplete="new-password"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="9" md="6" lg="6">
                <v-text-field
                  v-model="confirmPassword"
                  :outlined="true"
                  type="password"
                  :rules="[requiredRules]"
                  label="Confirme Contraseña"
                  hint="Confirme su contraseña"
                  autocomplete="new-password"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-file-input
                  :multiple="false"
                  :show-size="true"
                  label="Avatar"
                  prepend-icon="mdi-camera"
                  accept="image/png, image/jpeg"
                  placeholder="Selecciona la imagen de tu avatar"
                  outlined
                  @change="handleInput"
                 
                ></v-file-input>
              </v-col>
              <v-col cols="12" sm="9" md="6" lg="6">
                <v-text-field
                  v-model="oldPassword"
                  :outlined="true"
                  type="password"
                  :rules="[requiredRules]"
                  label="Contraseña actual"
                  hint="Introduzca su contraseña actual"
                  autocomplete="current-password"
                ></v-text-field>
              </v-col>
            </v-row>

            <v-row justify="center">
              <v-col>
                <v-btn color="primary" type="submit">Guardar cambios</v-btn>
              </v-col>
              <v-col>
                <v-btn color="error" @click="dialogDelete = true"
                  >Dar de baja</v-btn
                >
              </v-col>
            </v-row>
          </v-form>
        </v-col>
      </v-row>
    </div>
    <v-dialog v-model="dialogDelete" max-width="500px">
      <v-card>
        <v-card-title class="headline"
          >Estás seguro que quieres darte de baja?</v-card-title
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
  </div>
</template>

<script lang="ts">
import { authService } from "@/services/auth.service";
import { usersService } from "@/services/users.service";
import { Component, Vue, Watch } from "vue-property-decorator";

@Component
export default class UpdateUser extends Vue {
  public isValid = true;
  public lastName = "";
  public firstName = "";
  public email = "";
  public ext = "";
  public phone = "";
  public avatar = "";
  public newPassword = "";
  public confirmPassword = "";
  public oldPassword = "";
  public showLoading = false;
  public dialogDelete = false;

  @Watch("dialogDelete")
  public dialogDeleteWatch(val: any) {
    val || this.closeDelete();
  }
  public closeDelete() {
    console.log(this.avatar);
    this.dialogDelete = false;
  }
  public deleteItemConfirm() {
    this.$spinner.showSpinner();
    const user = this.$store.getters["user"];
    usersService
      .delete(user.id)
      .then((response: any) => {
        this.$store.dispatch("clear");
        this.$router.push({ name: "Login" });
      })
      .finally(this.$spinner.removeSpinner());
    this.closeDelete();
  }
  public requiredRules = (v: string) => !!v || "Este campo es obligatorio";
  public passwordsRules = (v: string) =>
    v == this.newPassword || "Las contraseñas no coinciden";
  public emailRules = (v: string) =>
    !v ||
    /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) ||
    "El email debe ser valido";

  public passwordRules = [
    (value: string) => !!value || "Please type password.",
    (value: string) => (value && value.length >= 6) || "minimum 6 characters",
  ];
  public sizeRules = [
    (value: any) =>
      !value || value.size < 2000000 || "Avatar size should be less than 2 MB!",
  ];

  public confirmPasswordRules = [
    (value: string) => !!value || "type confirm password",
    (value: string) =>
      value === this.newPassword || "The password confirmation does not match.",
  ];

private handleInput(input: any) {
      if (input.length > 0) {
        let fileName = '';
        fileName = input[0].name;
        if (fileName.lastIndexOf('.') <= 0) {
          return;
        }
          const fr = new FileReader();
          fr.readAsDataURL(input[0]);
          fr.addEventListener('load', () => {
            this.avatar= input[0];
          });
      } else if (input.length ===  0) {
            this.avatar= '';
      }
    }




  public mounted() {
    const user = this.$store.getters["user"];
    this.ext = user.ext;
    this.phone = user.phoneNumber;
    this.firstName = user.firstName;
    this.lastName = user.lastName;
    this.email = user.email;
  }

  public update(): void {
    this.showLoading = true;
    this.$spinner.showSpinner();
    console.log(this.avatar);
   
    const user = {
      FirstName: this.firstName,
      LastName: this.lastName,
      Email: this.email,
      OldPassword: this.oldPassword,
      NewPassword: this.newPassword,
      Ext: this.ext,
      Avatar: this.avatar,
      Phone: this.phone,
    };
    authService
      .update(user)
      .then(() => {
        console.log(user);
        this.$store.dispatch("setUser", user);
        this.$router.push({ name: "Home" });
      })
      .catch((error: Error) => {
        console.log(error);
      })
      .finally(() => {
        this.showLoading = false;
        this.$spinner.removeSpinner();
      });
  }
}
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
.padding_left {
  padding-left: 25px;
}
.flex {
  display: flex;
}
.widthExt {
  width: 150px;
  margin-right: 15px;
}
.updateUser {
  padding: 10px;
}
.padding {
  padding: 60px;
}
.shadow {
  background-color: white;
  box-shadow: 0 3px 1px -2px rgba(0, 0, 0, 0.2), 0 2px 2px 0 rgba(0, 0, 0, 0.14),
    0 1px 5px 0 rgba(0, 0, 0, 0.12);
}
</style>
