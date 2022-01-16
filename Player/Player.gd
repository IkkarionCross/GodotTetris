extends KinematicBody2D


var velocity = Vector2.ZERO
const MAX_SPEED = 150
const ACCELERATION = 500
const FRICTION = 500

func _ready():
	print("Heloo World")


func _physics_process(delta):
	var inputVector = Vector2.ZERO
	inputVector.x = Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	inputVector.y = Input.get_action_strength("ui_down") - Input.get_action_strength("ui_up")
	inputVector = inputVector.normalized()
	
	if inputVector != Vector2.ZERO:
		velocity = velocity.move_toward(inputVector * MAX_SPEED, ACCELERATION * delta)
	else:
		velocity = velocity.move_toward(Vector2.ZERO, FRICTION * delta)
		
		
	# print(velocity)
	
	# move_and_collide(velocity * delta)
	velocity = move_and_slide(velocity)
	
