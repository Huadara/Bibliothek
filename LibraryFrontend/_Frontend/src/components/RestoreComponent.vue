<template>
  <div>
    <h1>Restore Book</h1>
      <b-container>
        <b-row>
          <b-col>
            <h4>Book:</h4>
          </b-col>
          <b-col>
            <b-dropdown></b-dropdown>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <h4>Address:</h4>
          </b-col>
          <b-col>
            <input>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-button :variant="primary">Buy Book</b-button>
          </b-col>
        </b-row>
      </b-container>
    <span>{{msg}}</span>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'BuyBookComponent',
  data () {
    return {
      customer_id: '',
      book_id: '',
      store_id: '',
      amount: '',
      msg: ''
    }
  },
  methods: {
    buyBook () {
      axios.post('http://localhost:5000/library/books/BuyBook', {
        customer_id: this.customer_id,
        book_id: this.book_id,
        store_id: this.store_id,
        amount: this.amount
      }).then(function (response) {
        if (response != null) console.log('response')
        self.msg = 'Book has been bought'
      }).catch(function (error) {
        if (error != null) console.log('error')
        self.msg = 'Entries invalid'
      })
    }
  }
}
</script>

<style scoped>

</style>
