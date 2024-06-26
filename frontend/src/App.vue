<template>
  <!-- Główny kontener aplikacji -->
  <div id="app">
    <!-- Widok routera, tutaj będą renderowane komponenty na podstawie aktualnej ścieżki -->
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  // Definicja komponentu głównego aplikacji
  name: 'App'
};
</script>

<style>
/* Tutaj można dodać globalne style dla aplikacji */
</style>
