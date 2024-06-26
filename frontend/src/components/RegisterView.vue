<template>
  <!-- Główny kontener dla widoku rejestracji -->
  <div class="auth-container">
    <!-- Pudełko autoryzacyjne zawierające formularz rejestracyjny -->
    <div class="auth-box">
      <h2>Register</h2>
      <!-- Formularz rejestracyjny z obsługą zdarzenia submit -->
      <form @submit.prevent="register">
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
        <div class="form-group">
          <!-- Pole wprowadzania potwierdzenia hasła -->
          <label>Confirm Password:</label>
          <input type="password" v-model="confirmPassword" required>
          <!-- Komunikat o błędzie dla potwierdzenia hasła -->
          <div v-if="confirmPasswordError" class="error-message">{{ confirmPasswordError }}</div>
        </div>
        <!-- Przycisk przesyłania formularza -->
        <button type="submit" class="submit-button">Register</button>
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
      // Dane dla formularza rejestracyjnego
      email: '',
      password: '',
      confirmPassword: '',
      // Zmienne do przechowywania komunikatów o błędach
      emailError: '',
      passwordError: '',
      confirmPasswordError: ''
    };
  },
  methods: {
    // Metoda do walidacji adresu email
    validateEmail() {
      // Wyrażenie regularne do sprawdzania poprawności adresu email
      const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
      return emailPattern.test(this.email);
    },
    // Metoda do walidacji hasła
    validatePassword() {
      // Wymagania dotyczące hasła
      const minLength = 6;
      const hasDigit = /\d/;
      const hasUppercase = /[A-Z]/;
      const hasLowercase = /[a-z]/;

      // Sprawdzenie, czy hasło spełnia wymagania
      return this.password.length >= minLength &&
             hasDigit.test(this.password) &&
             hasUppercase.test(this.password) &&
             hasLowercase.test(this.password);
    },
    // Metoda do rejestracji użytkownika
    async register() {
      // Resetowanie komunikatów o błędach
      this.emailError = '';
      this.passwordError = '';
      this.confirmPasswordError = '';

      // Walidacja emaila
      if (!this.validateEmail()) {
        this.emailError = 'Invalid email address.';
      }
      // Walidacja hasła
      if (!this.validatePassword()) {
        this.passwordError = 'Password must be at least 6 characters long, contain at least one digit, one uppercase letter, and one lowercase letter.';
      }
      // Sprawdzenie, czy hasła się zgadzają
      if (this.password !== this.confirmPassword) {
        this.confirmPasswordError = 'Passwords do not match.';
      }

      // Jeśli występują błędy, przerwij rejestrację
      if (this.emailError || this.passwordError || this.confirmPasswordError) {
        return;
      }

      try {
        // Wysłanie żądania rejestracji do serwera
        const response = await axios.post('/api/Account/Register', {
          email: this.email,
          password: this.password,
          confirmPassword: this.confirmPassword
        });
        console.log('Registration successful:', response.data);
        // Przekierowanie na stronę logowania po udanej rejestracji
        this.$router.push({ name: 'login' });
      } catch (error) {
        // Obsługa błędów rejestracji
        console.error('Registration failed:', error.response ? error.response.data : error.message);
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
  margin-top: 5px; /* Dodaje górny margines */
}
</style>
