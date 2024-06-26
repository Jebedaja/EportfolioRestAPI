<template>
  <div class="add-portfolio">
    <h2>Add Portfolio</h2>
    <form @submit.prevent="addPortfolio">
      <div class="form-group">
        <label for="portfolioName">Portfolio Name:</label>
        <input type="text" id="portfolioName" v-model="portfolio.portfolioName" required>
      </div>
      <div class="form-group">
        <label for="description">Description:</label>
        <textarea id="description" v-model="portfolio.description" required></textarea>
      </div>
      <div class="form-group">
        <label for="youTubeLink">YouTube Link:</label>
        <input type="text" id="youTubeLink" v-model="portfolio.youTubeLink" required>
      </div>
      <button type="submit">Add Portfolio</button>
      <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      portfolio: {
        portfolioName: '',
        description: '',
        youTubeLink: '',
        dateAdded: new Date()  // Ustawienie daty na obecną
      },
      errorMessage: ''
    };
  },
  methods: {
    async addPortfolio() {
      try {
        const response = await fetch('/api/Account/Portfolio', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem('token')}`
          },
          body: JSON.stringify(this.portfolio)
        });
        if (response.ok) {
          this.$router.push({ name: 'portfolio' }); // Przekierowanie do widoku portfolio po dodaniu
        } else {
          console.error('Failed to add portfolio');
          this.errorMessage = 'Failed to add portfolio';
        }
      } catch (error) {
        console.error('Error adding portfolio:', error);
        this.errorMessage = `Error adding portfolio: ${error.message}`;
      }
    }
  }
};
</script>

<style scoped>
.add-portfolio {
  text-align: center;
  padding: 20px;
  background: white;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  width: 80%;
  max-width: 800px;
  margin: auto;
}

h2 {
  margin-bottom: 20px;
  color: #333;
}

.form-group {
  margin-bottom: 20px;
}

label {
  display: block;
  margin-bottom: 8px;
  color: #555;
}

input[type="text"],
textarea {
  width: 100%;
  padding: 8px;
  font-size: 16px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

button {
  background-color: #b6b6b6;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #3b3c3c;
}

.error-message {
  margin-top: 20px;
  color: red;
}
</style>
