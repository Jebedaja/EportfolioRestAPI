<template>
  <div class="auth-container">
    <div class="auth-box">
      <h2>Login</h2>
      <form @submit.prevent="login">
        <div class="form-group">
          <label>Email:</label>
          <input type="email" v-model="email" required>
        </div>
        <div class="form-group">
          <label>Password:</label>
          <input type="password" v-model="password" required>
        </div>
        <div class="form-group checkbox">
          <label>
            <input type="checkbox" v-model="rememberMe"> Remember Me
          </label>
        </div>
        <button type="submit" class="submit-button">Login</button>
      </form>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      email: '',
      password: '',
      rememberMe: false
    };
  },
  methods: {
    async login() {
      try {
        const response = await axios.post('/api/Account/Login', {
          email: this.email,
          password: this.password,
          rememberMe: this.rememberMe
        });
        // Handle the token received from the server
        localStorage.setItem('token', response.data.token);
        this.$router.push({ name: 'portfolio' });
      } catch (error) {
        console.error('Login failed', error);
      }
    }
  }
};
</script>

<style scoped>
.auth-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #e0e0e0; /* Jasno szare tło */
}

.auth-box {
  text-align: center;
  padding: 20px;
  background: white;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  width: 300px;
}

h2 {
  margin-bottom: 20px;
  color: #333;
}

.form-group {
  margin-bottom: 15px;
}

input[type="email"],
input[type="password"] {
  width: calc(100% - 22px);
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.checkbox {
  display: flex;
  align-items: center;
  justify-content: center;
}

.submit-button {
  padding: 10px 20px;
  color: white;
  background-color: #b6b6b6;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.submit-button:hover {
  background-color: #3b3c3c;
}
</style>
