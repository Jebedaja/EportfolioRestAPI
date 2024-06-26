<template>
  <!-- Główny kontener dla widoku logowania -->
  <div class="auth-container">
    <!-- Pudełko autoryzacyjne zawierające formularz logowania -->
    <div class="auth-box">
      <h2>Login</h2>
      <!-- Formularz logowania z obsługą zdarzenia submit -->
      <form @submit.prevent="login">
        <div class="form-group">
          <!-- Pole wprowadzania emaila -->
          <label>Email:</label>
          <input type="email" v-model="email" required>
          <!-- Komunikat o błędzie dla emaila -->
          <div v-if="emailError" class="error-message">{{ emailError }}</div>
        </div>
        <div class="form-group">
          <!-- Pole wprowadzania hasła -->
          <label>Password:</label>
          <input type="password" v-model="password" required>
          <!-- Komunikat o błędzie dla hasła -->
          <div v-if="passwordError" class="error-message">{{ passwordError }}</div>
        </div>
        <div class="form-group checkbox">
          <!-- Pole wprowadzania opcji "Remember Me" -->
          <label>
            <input type="checkbox" v-model="rememberMe"> Remember Me
          </label>
        </div>
        <!-- Przycisk przesyłania formularza -->
        <button type="submit" class="submit-button">Login</button>
        <!-- Komunikat o błędzie logowania -->
        <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
      </form>
    </div>
  </div>
</template>

<script>
// Importowanie biblioteki axios do wykonywania żądań HTTP
import axios from 'axios';

export default {
  data() {
    return {
      // Dane dla formularza logowania
      email: '',
      password: '',
      rememberMe: false,
      errorMessage: '',
      emailError: '',
      passwordError: ''
    };
  },
  methods: {
    // Metoda do logowania użytkownika
    async login() {
      // Resetowanie komunikatów o błędach
      this.errorMessage = '';
      this.emailError = '';
      this.passwordError = '';

      // Sprawdzenie, czy pola email i hasło są wypełnione
      if (!this.email || !this.password) {
        this.errorMessage = 'Please fill in all fields.';
        return;
      }

      try {
        // Wysłanie żądania logowania do serwera
        const response = await axios.post('/api/Account/Login', {
          email: this.email,
          password: this.password,
          rememberMe: this.rememberMe
        });
        // Zapisanie tokenu w localStorage
        localStorage.setItem('token', response.data.token);
        // Przekierowanie na stronę portfolio po udanym logowaniu
        this.$router.push({ name: 'portfolio' });
      } catch (error) {
        // Obsługa błędów logowania
        console.error('Login failed', error.response ? error.response.data : error.message);
        if (error.response && error.response.status === 400) {
          this.errorMessage = 'Invalid login attempt. Please check your email and password.';
        } else {
          this.errorMessage = 'An error occurred. Please try again later.';
        }
      }
    }
  }
};
</script>

<style scoped>
/* Stylizuje główny kontener, aby centrować zawartość pionowo i poziomo oraz ustawić tło */
.auth-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #e0e0e0;
}

/* Stylizuje pudełko autoryzacyjne */
.auth-box {
  text-align: center; /* Centruje tekst */
  padding: 20px; /* Dodaje wewnętrzny margines */
  background: white; /* Ustawia tło na białe */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Dodaje cień wokół pudełka */
  border-radius: 8px; /* Zaokrągla rogi */
  width: 300px; /* Ustawia szerokość pudełka */
}

/* Stylizuje nagłówek */
h2 {
  margin-bottom: 20px; /* Dodaje dolny margines */
  color: #333; /* Ustawia kolor tekstu na ciemnoszary */
}

/* Stylizuje grupy formularza */
.form-group {
  margin-bottom: 15px; /* Dodaje dolny margines */
}

/* Stylizuje pola wprowadzania danych */
input[type="email"],
input[type="password"] {
  width: calc(100% - 22px); /* Ustawia szerokość pól */
  padding: 10px; /* Dodaje wewnętrzny margines */
  border: 1px solid #ccc; /* Ustawia kolor ramki */
  border-radius: 4px; /* Zaokrągla rogi */
}

/* Stylizuje grupę opcji "Remember Me" */
.checkbox {
  display: flex; /* Ustawia wyświetlanie na flex */
  align-items: center; /* Centruje elementy w pionie */
  justify-content: center; /* Centruje elementy w poziomie */
}

/* Stylizuje przycisk przesyłania formularza */
.submit-button {
  padding: 10px 20px; /* Dodaje wewnętrzny margines */
  color: white; /* Ustawia kolor tekstu na biały */
  background-color: #b6b6b6; /* Ustawia tło na jasnoszare */
  border: none; /* Usuwa ramkę */
  border-radius: 5px; /* Zaokrągla rogi */
  cursor: pointer; /* Ustawia kursor na wskaźnik */
  transition: background-color 0.3s ease; /* Dodaje płynne przejście zmiany koloru tła */
}

/* Zmienia kolor tła przycisku przy najechaniu */
.submit-button:hover {
  background-color: #3b3c3c; /* Ustawia tło na ciemnoszare */
}

/* Stylizuje komunikaty o błędach */
.error-message {
  color: red; /* Ustawia kolor tekstu na czerwony */
  margin-top: 10px; /* Dodaje górny margines */
}
</style>
