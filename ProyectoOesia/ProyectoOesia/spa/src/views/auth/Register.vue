<template>
  <v-form action class="form" v-model="isValid" @submit.prevent="register">
    <v-row>
      <v-col cols="6">
        <v-text-field
          v-model="firstName"
          :outlined="true"
          label="Nombre"
          :rules="[requiredRules]"
          hint="Introduzca su nombre"
        ></v-text-field>
      </v-col>
      <v-col cols="6">
        <v-text-field
          v-model="lastName"
          :rules="[requiredRules]"
          :outlined="true"
          label="Apellido"
          hint="Introduzca su apellido"
        ></v-text-field>
      </v-col>
      <v-col cols="6">
        <v-text-field
          v-model="email"
          :outlined="true"
          :rules="[requiredRules, emailRules]"
          label="Email"
          hint="Introduzca su correo electrónico"
        ></v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6">
        <v-text-field
          v-model="password"
          type="password"
          :rules="[requiredRules]"
          :outlined="true"
          label="Contraseña"
          hint="Introduzca su contraseña"
        ></v-text-field>
      </v-col>
      <v-col cols="6">
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

    <v-checkbox
      v-model="company"
      label="Crear perfil como empresa"
    ></v-checkbox>
    <create-company v-if="company" />

    <v-btn color="primary" type="submit" :disabled="!isValid">Registrar</v-btn>
  </v-form>
</template>

<script lang="ts">
import CreateCompany from "@/components/CreateCompany.vue";
import { authService } from "@/services/auth.service";
import { Component, Vue } from "vue-property-decorator";

@Component({
  components: {
     "create-company": CreateCompany,
  },
})
export default class Login extends Vue {
  public email = "";
  public isValid = true;
  public password = "";
  public firstName = "";
  public lastName = "";
  public confirmPassword = "";
  public company = false;
  public showLoading = false;

  public requiredRules = (v: any) => !!v || "Required";
  public emailRules = (v: any) =>
    !v ||
    /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) ||
    "E-mail must be valid";

  public register(): void {
    this.showLoading = true;
    if (this.company) {
      authService
        .register(this.email, this.password)
        .then(() => {
          this.$router.push({ name: "Login" });
        })
        .catch((error: Error) => {
          console.log(error);
        })
        .finally(() => {
          this.showLoading = false;
        });
    } else {
      console.log(this.company);
    }
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
</style>
