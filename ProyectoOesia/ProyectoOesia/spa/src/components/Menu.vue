<template>
  <div v-if="token">
    <v-navigation-drawer
      v-model="drawer"
      :color="color"
      :expand-on-hover="expandOnHover"
      :mini-variant="miniVariant"
      :permanent="permanent"
      :src="url"
      fixed
      dark
    >
      <v-list dense nav class="py-0 list-container">
        <v-list-item two-line :class="miniVariant && 'px-0'">
          <v-list-item-avatar>
            <img :src="'data:image/jpg;base64,'+ user.avatar " srcset="..\assets\profile-user.png"  />
          </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title v-if="role!='Admin'">{{user.name}}</v-list-item-title>
            <v-list-item-subtitle v-if="role=='Admin'">{{user.name}} | Administrador</v-list-item-subtitle>
            <v-list-item-subtitle>{{user.email}}</v-list-item-subtitle>
            <v-list-item-subtitle v-if="user.company">{{user.company.companyName}}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>
        <v-list-item :to="'/'+item.path" v-for="item in items" :key="item.title" link>
          
            <v-list-item-icon>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item-content>
        </v-list-item>
        
        <v-list-item class="logout" @click="logout" link>
          
            <v-list-item-icon>
              <v-icon>mdi-logout</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title>Cerrar sesion</v-list-item-title>
            </v-list-item-content>
         
        </v-list-item>
      </v-list>
     
    </v-navigation-drawer>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Menu extends Vue {
  public drawer = true;
  public color = "primary";
  public right = false;
  public permanent = true;
  public miniVariant = true;
  public expandOnHover = true;
  public url = "https://i.stack.imgur.com/xfn1X.jpg";
  
   get user(): any {
    return this.$store.getters["user"];
  }
   get token(): any {
    return this.$store.getters["token"];
  }
   get role(): any {
    return this.$store.getters["rol"];
  }
  public get hasCompany(): boolean {
    return this.user?.company?.id != -1;
  }

  public get items() {
    console.log(this.role);
    if(this.role == 'Admin') {
      return [
        { title: "Gestionar compañias", icon: "mdi-home", path:"admin" },
        { title: "Gestionar usuarios", icon: "mdi-account-settings", path:"users" },
      ];
    }
    else if(this.role == 'Worker' && this.hasCompany) {
      return [
        { title: "Ver contactos", icon: "mdi-account-settings", path:"contacts" },
        { title: "Modificar empresa", icon: "mdi-account-settings", path:"update" },
        { title: "Modificar perfil", icon: "mdi-account-settings", path:"profile" },
      ];
    } 
    else if(this.role == 'Worker' && !this.hasCompany) {
      return [
        { title: "Crear empresa", icon: "mdi-account-settings", path:"new" },
        { title: "Modificar perfil", icon: "mdi-account-settings", path:"profile" },
      ];
    } 
    else {
      return [
        { title: "Buscar oferta", icon: "mdi-account-settings", path:"companies" },
        { title: "Modificar perfil", icon: "mdi-account-settings", path:"profile" },
      ];
    }
  }

  public logout(): void {
    this.$store.dispatch("clear");
    this.$router.push({ name: 'Login' });
  }

}
</script>

<style lang="scss" scoped>
.list-container {
  position: relative;
  height: 100%;
}
.logout {
  position: absolute;
  bottom: 20px;
  width: 100%;
}
</style>
