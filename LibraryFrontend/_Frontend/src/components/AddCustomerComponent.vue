<template>
  <div>
    <h1>Add Customer</h1>
    <b-container>
      <b-row>
        <b-col>
          <h4>Firstname:</h4>
        </b-col>
        <b-col>
          <input v-model="firstname">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Lastname:</h4>
        </b-col>
        <b-col>
          <input v-model="lastname">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Address:</h4>
        </b-col>
        <b-col>
          <input v-model="address">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Credit Nr:</h4>
        </b-col>
        <b-col>
          <input v-model="credit_number">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button :variant="primary" @click="addCustomer">Add Customer</b-button>
        </b-col>
      </b-row>
    </b-container>
    <span>{{msg}}</span>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'AddCustomerComponent',
  data () {
    return {
      firstname: '',
      lastname: '',
      address: '',
      credit_number: '',
      msg: ''
    }
  },
  methods: {
    addCustomer () {
      const self = this
      axios.post('http://localhost:5000/library/customers', {
        'firstname': this.firstname,
        'lastname': this.lastname,
        'address': this.address,
        'credit_number': this.credit_number
      }).then(function (response) {
        if (response != null) console.log('response')
        self.msg = 'Add successful'
      }).catch(function (error) {
        if (error != null) console.log('error')
        self.msg = 'Please check your entries'
      })
    }
  }
}
</script>

<style scoped>

</style>
