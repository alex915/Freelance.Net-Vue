<template>
  <div>
    <v-row justify="center">
      <v-col cols="11" md="10" lg="8">
        <v-form
          action
          class="form"
          v-model="isValid"
          @submit.prevent="register"
        >
          <v-row justify="center" class="pb-6">
            <v-col cols="12">
              <h2>¡Registrate y empieza!</h2>
            </v-col>
            <v-col cols="12">
              <h4>Escribe tus datos</h4>
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
                v-model="password"
                type="password"
                :rules="[requiredRules]"
                :outlined="true"
                label="Contraseña"
                hint="Introduzca su contraseña"
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
          </v-row>
          <v-row dense justify="center">
            <v-col cols="11" sm="8" md="6" lg="6" class="padding_left">
              <v-switch
                v-model="company"
                label="Crear perfil como empresa/autonomo. Esto le permitira anunciarse en la web"
              ></v-switch>
            </v-col>
            <v-col cols="11" sm="8" md="6" lg="6" class="padding_left">
              <v-checkbox
                v-model="condition"
                label="Acepto las condiciones de uso y las politicas de privacidad del servicio."
              ></v-checkbox>
            </v-col>
          </v-row>
          <v-row justify="center">
            <v-col>
              <v-btn
                color="primary"
                type="submit"
                :disabled="!isValid && !condition"
                >Crear una cuenta</v-btn
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
  public condition = false;
  public firstName = "";
  public lastName = "";
  public email = "";
  public ext = "";
  public phone = "";
  public password = "";
  public confirmPassword = "";
  public company = false;
  public showLoading = false;

  public requiredRules = (v: string) => !!v || "Este campo es obligatorio";
  public passwordsRules = (v: string) =>
    v == this.password || "Las contraseñas no coinciden";
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
      value === this.password || "The password confirmation does not match.",
  ];

  public register(): void {
    this.showLoading = true;
    const user = {
      FirstName: this.firstName,
      LastName: this.lastName,
      Email: this.email,
      Password: this.password,
      Ext: this.ext,
      Phone: this.phone,
      Company: this.company,
    };
    authService
      .register(user)
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
