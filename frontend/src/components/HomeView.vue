<template>
  <!-- Główny kontener dla widoku strony głównej -->
  <div class="home-container">
    <!-- Pudełko powitalne zawierające tytuł i linki do logowania i rejestracji -->
    <div class="welcome-box">
      <h1>Welcome to E-Portfolio</h1>
      <div class="links">
        <!-- Link do strony logowania -->
        <router-link to="/login" class="link-button">Login</router-link>
        <!-- Link do strony rejestracji -->
        <router-link to="/register" class="link-button">Register</router-link>
      </div>
    </div>
  </div>
</template>

<script>
// Definiuje komponent HomeView
export default {
  name: 'HomeView'
};
</script>

<style scoped>
/* Stylizuje główny kontener, aby centrować zawartość pionowo i poziomo oraz ustawić tło */
.home-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #e0e0e0; /* Jasnoszary kolor tła */
}

/* Stylizuje pudełko powitalne */
.welcome-box {
  text-align: center; /* Centruje tekst */
  padding: 20px; /* Dodaje wewnętrzny margines */
  background: white; /* Ustawia tło na białe */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Dodaje cień wokół pudełka */
  border-radius: 8px; /* Zaokrągla rogi */
}

/* Stylizuje nagłówek */
h1 {
  font-size: 2.5em; /* Ustawia rozmiar czcionki */
  margin-bottom: 20px; /* Dodaje dolny margines */
  color: #333; /* Ustawia kolor tekstu na ciemnoszary */
}

/* Stylizuje kontener z linkami */
.links {
  display: flex; /* Ustawia flexbox do rozmieszczenia linków */
  justify-content: center; /* Centruje linki poziomo */
  gap: 15px; /* Dodaje odstęp między linkami */
}

/* Stylizuje przyciski linków */
.link-button {
  display: inline-block; /* Ustawia elementy jako blokowe w linii */
  padding: 10px 20px; /* Dodaje wewnętrzny margines */
  color: white; /* Ustawia kolor tekstu na biały */
  background-color: #b6b6b6; /* Ustawia tło na jasnoszare */
  border: none; /* Usuwa ramkę */
  border-radius: 5px; /* Zaokrągla rogi */
  text-decoration: none; /* Usuwa podkreślenie */
  font-size: 1em; /* Ustawia rozmiar czcionki */
  transition: background-color 0.3s ease; /* Dodaje płynne przejście zmiany koloru tła */
}

/* Zmienia kolor tła przycisku linku przy najechaniu */
.link-button:hover {
  background-color: #3b3c3c; /* Ustawia tło na ciemnoszare */
}
</style>
