using Godot;
using System;

public partial class Bola : CharacterBody2D
{
	private Vector2 velocity = new Vector2(300, 300); // Velocidad inicial
	private Vector2 startPosition; // Posición inicial de la bola
	private Label scoreLabel; // Referencia al marcador
	private int scoreLeft = 0; // Puntuación izquierda
	private int scoreRight = 0; // Puntuación derecha

	public override void _Ready()
	{
		startPosition = Position; // Guardamos la posición inicial
		scoreLabel = GetNode<Label>("/root/Main/LabelPuntuacion"); // Ruta del Label en la jerarquía
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += velocity * (float)delta;

		var screenSize = GetViewportRect().Size;

		// Rebote en bordes superior e inferior
		if (Position.Y <= 0 || Position.Y >= screenSize.Y)
		{
			velocity.Y = -velocity.Y;
		}

		// Si la bola sale por la izquierda
		if (Position.X <= 0)
		{
			scoreRight++; // Aumenta el puntaje del jugador derecho
			ReiniciarBola();
		}
		// Si la bola sale por la derecha
		else if (Position.X >= screenSize.X)
		{
			scoreLeft++; // Aumenta el puntaje del jugador izquierdo
			ReiniciarBola();
		}
	}

	private void ReiniciarBola()
	{
		Position = startPosition; // Volvemos la bola al centro
		velocity = new Vector2(GD.Randf() > 0.5 ? 300 : -300, GD.Randf() * 200 - 100); // Dirección aleatoria
		scoreLabel.Text = $"{scoreLeft} - {scoreRight}"; // Actualiza el marcador
	}
}
