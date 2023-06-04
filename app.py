from flask import Flask, request
import pika

app = Flask(__name__)

@app.route('/send-message', methods=['POST'])
def send_message():
    message = request.json  # Obtener el mensaje del cuerpo de la solicitud

    # Configurar la conexión con RabbitMQ
    connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
    channel = connection.channel()

    # Enviar el mensaje al exchange
    channel.basic_publish(exchange='my_exchange', routing_key='', body=message['content'])

    connection.close()

    return 'Mensaje enviado correctamente'

if __name__ == '__main__':
    app.run()
