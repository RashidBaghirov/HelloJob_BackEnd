window.addEventListener('load', function() {
    var addButton = document.querySelector('.plus_btn');
    var minusButton = document.querySelector('.minus_btn');
    var addInputContainer = document.querySelector('.adding');
    var hiddenInput = document.querySelector('.hidden_Input');
  
    addButton.addEventListener('click', function(e) {
        e.preventDefault();
      var addInput = addInputContainer.querySelector('.add_input');
      var newInputValue = addInput.value.trim();
      if (newInputValue !== '') {
        var currentValue = hiddenInput.value.trim();
        var separator = currentValue === '' ? '' : '/';
        hiddenInput.value = currentValue + separator + newInputValue;
        addInput.value = '';
      }
    });

    minusButton.addEventListener('click', function(e) {
        e.preventDefault();
        var currentValue = hiddenInput.value.trim();
        if (currentValue !== '') {
          var values = currentValue.split('/');
          var newValues = values.slice(0, -1);
          hiddenInput.value = newValues.join('/');
        }
      });
  });