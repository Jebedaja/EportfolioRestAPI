// Importowanie funkcji do tworzenia aplikacji Vue
import { createApp } from 'vue';
// Importowanie głównego komponentu aplikacji
import App from './App.vue';
// Importowanie konfiguracji routera
import router from './router';

// Tworzenie instancji aplikacji Vue, dodanie routera do aplikacji i montowanie aplikacji na elemencie HTML z id="app"
createApp(App)
  .use(router) // Dodanie obsługi routera do aplikacji
  .mount('#app'); // Montowanie aplikacji na elemencie o id "app"
