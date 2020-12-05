<template>
  <div v-if="token">
    <v-navigation-drawer
      v-model="drawer"
      :color="color"
      :expand-on-hover="expandOnHover"
      :mini-variant="miniVariant"
      :permanent="permanent"
      src="https://cdn.vuetifyjs.com/images/backgrounds/bg-2.jpg"
      fixed
      dark
    >
      <v-list dense nav class="py-0">
        <v-list-item two-line :class="miniVariant && 'px-0'">
          <v-list-item-avatar>
            <img src="https://randomuser.me/api/portraits/men/81.jpg" />
          </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title>{{user.name}}</v-list-item-title>
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
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Menu extends Vue {
  public drawer = true;
  public items = [
    { title: "Novedades", icon: "mdi-home", path:"" },
    { title: "Contactos", icon: "mdi-email", path:"" },
    { title: "Favoritos", icon: "mdi-star", path:"" },
    { title: "Perfil", icon: "mdi-account-settings", path:"profile" },
  ];
  public color = "primary";
  public right = false;
  public permanent = true;
  public miniVariant = true;
  public expandOnHover = true;
  public background = false;
  
   get user(): any {
    return this.$store.getters["getUser"];
  }
   get token(): any {
    return this.$store.getters["getToken"];
  }

}
</script>

<style lang="scss" scoped>
</style>
