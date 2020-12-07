<template>
 <section class="register">
    <div class="padding_top">
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
                  autocomplete="username"
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
                  autocomplete="new-password"
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
                  autocomplete="new-password"
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
                  color="#2f9fe9"
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
  </div>
 </section>
</template>

<script lang="ts">
import { authService } from "@/services/auth.service";
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Register extends Vue {
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
    this.$spinner.showSpinner();
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
        this.$spinner.removeSpinner();
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
.padding_top {
  display: flex;
  justify-content: center;
  align-items: center;
}
.padding_top > div {
  color: white;
  padding: 40px 20px;
  background: rgb(17, 37, 87);
  background: linear-gradient(
    308deg,
    rgba(17, 37, 87, 1) 4%,
    rgba(17, 45, 154, 1) 24%,
    rgba(61, 160, 228, 1) 59%,
    rgba(18, 155, 241, 1) 100%
  );
  border-radius: 6px;
  box-shadow: 1px 2px 12px #ddd;
}
/* @media (min-width: 950px) {
  .padding_top > div {
     margin-top: 8vh; 
  }
} */
::v-deep input,
::v-deep label,
::v-deep fieldset,
::v-deep .theme--light {
  color: white !important;
}
.register{
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
