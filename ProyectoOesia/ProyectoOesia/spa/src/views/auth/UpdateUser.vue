<template>
  <div>
    <v-row justify="center">
      <v-col cols="11" md="10" lg="8">
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
            <v-col cols="11" sm="8" md="6" lg="6">
              <v-text-field
                v-model="firstName"
                :outlined="true"
                label="Nombre"
                :rules="[requiredRules]"
                hint="Introduzca su nombre"
              ></v-text-field>
            </v-col>
            <v-col cols="11" sm="8" md="6" lg="6">
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
            <v-col cols="11" sm="8" md="7" lg="7">
              <v-text-field
                v-model="email"
                :outlined="true"
                :rules="[requiredRules, emailRules]"
                label="Email"
                hint="Introduzca su correo electrónico"
              ></v-text-field>
            </v-col>
            <v-col cols="11" sm="8" md="4" lg="4">
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
            <v-col cols="11" sm="8" md="6" lg="6">
              <v-text-field
                v-model="newPassword"
                type="password"
                :rules="[requiredRules]"
                :outlined="true"
                label="Nueva contraseña"
                hint="Introduzca una nueva contraseña"
              ></v-text-field>
            </v-col>
            <v-col cols="11" sm="8" md="6" lg="6">
              <v-text-field
                v-model="confirmPassword"
                :outlined="true"
                type="password"
                :rules="[requiredRules]"
                label="Confirme Contraseña"
                hint="Confirme su contraseña"
              ></v-text-field>
            </v-col>
            <v-col cols="11" sm="8" md="6" lg="6">
              <v-text-field
                v-model="oldPassword"
                :outlined="true"
                type="password"
                :rules="[requiredRules]"
                label="Contraseña actual"
                hint="Introduzca su contraseña actual"
              ></v-text-field>
            </v-col>
          </v-row>
          
          <v-row justify="center">
            <v-col>
              <v-btn
                color="primary"
                type="submit"
                
                >Guardar cambios</v-btn
              >
            </v-col>
          </v-row>
        </v-form>
      </v-col>
    </v-row>
  </div>
</template>

<script lang="ts">
import { authService } from "@/services/auth.service";
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Login extends Vue {
  public isValid = true;
  public lastName = "";
  public firstName = "";
  public email = "";
  public ext = "";
  public phone = "";
  public newPassword = "";
  public confirmPassword = "";
  public oldPassword = "";
  public showLoading = false;

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

  public confirmPasswordRules = [
    (value: string) => !!value || "type confirm password",
    (value: string) =>
      value === this.newPassword || "The password confirmation does not match.",
  ];

public mounted(){
  const user = this.$store.getters["getUser"];
  this.ext = user.ext;
  this.phone = user.phoneNumber;
  this.firstName = user.firstName;
  this.lastName = user.lastName;
  this.email = user.email;

}

  public update(): void {
    this.showLoading = true;
    const user = {
      FirstName: this.firstName,
      LastName: this.lastName,
      Email: this.email,
      OldPassword:this.oldPassword,
      NewPassword: this.newPassword,
      Ext: this.ext,
      Phone: this.phone,
    };
    authService
      .update(user)
      .then(() => {
         this.$router.push({ name: "Login" });
      })
      .catch((error: Error) => {
        console.log(error);
      })
      .finally(() => {
        this.showLoading = false;
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
</style>
