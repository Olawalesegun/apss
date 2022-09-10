function showDeletionModal(message, id) {
    let form = $('#delete-modal-form');

    form.attr('action', `${form.attr('action')}?id=${id}`);
    $('#delete-modal-message').text(message);

    $('#delete-modal').modal('show');
}