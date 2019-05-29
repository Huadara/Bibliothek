<template>
  <div>
    <h1>CUSTOMER</h1>
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
          <b-button :variant="primary">Add Customer</b-button>
        </b-col>
      </b-row>
    </b-container>
    <h1>{{addCustomer(firstname,lastname,address,credit_number)}}</h1>
  </div>
</template>

<script>
  export default {
    name: "AddCustomerComponent",
    data() {
      return {
        firstname: "",
        lastname: "",
        address: "",
        credit_number: ""
      }
    },
    methods: {
      addCustomer(firstname, lastname, address, creditNr) {
        const json = `{ "firstname": "${firstname}", "lastname": "${lastname}", "address": "${address}", "credit_number": "${creditNr}" }`;
        return json;
      }
    }
  }
</script>

<style scoped>

</style>
