using NUnit.Framework;
using UnityEditor;

public class PieceMovementTests
{
	private static string PieceConfigPathPrefix = "Assets/Level/ScriptableObjects/Pieces";

	[Test]
	public void PieceMovement_Knight()
	{
		Team team = Team.White;
		Team oppositeTeam = Team.Black;
		PieceType type = PieceType.Knight;
		BoardModel boardModel = new BoardModel();

		PieceConfig pieceConfig = AssetDatabase.LoadAssetAtPath<PieceConfig>($"{PieceConfigPathPrefix}/KnightConfig.asset");
		Assert.IsNotNull(pieceConfig, $"Cannot find PieceConfig ScriptableObject on {PieceConfigPathPrefix}!");
		
		PieceModel pieceModel = new PieceModel(team, type, pieceConfig, 0, 0);
		boardModel.SetCellOwnership(0, 0, team);

		// scenario 1: valid movement;
		// [ ][x][ ]
		// [ ][ ][x]
		// [K][ ][ ]

		Assert.IsTrue(boardModel.IsMovementAllowed(1, 2, pieceModel), "Failed scenario 1 movement!");
		Assert.IsTrue(boardModel.IsMovementAllowed(2, 1, pieceModel), "Failed scenario 1 movement!");

		// scenario 2: invalid movement - out of pattern;
		// [ ][ ][x]
		// [ ][ ][ ]
		// [K][ ][ ]

		Assert.IsFalse(boardModel.IsMovementAllowed(2, 2, pieceModel), "Failed scenario 2 movement!");

		// scenario 3: valid movement - blocked path;
		// [ ][ ][ ]
		// [O][O][x]
		// [K][O][ ]

		boardModel.SetCellOwnership(1, 0, oppositeTeam);
		boardModel.SetCellOwnership(1, 1, oppositeTeam);
		boardModel.SetCellOwnership(0, 1, oppositeTeam);
		Assert.IsTrue(boardModel.IsMovementAllowed(1, 2, pieceModel), "Failed scenario 3 movement!");
		boardModel.SetCellOwnership(1, 0, Team.None);
		boardModel.SetCellOwnership(1, 1, Team.None);
		boardModel.SetCellOwnership(0, 1, Team.None);

		// scenario 4: invalid movement - blocked destination;
		// [ ][ ][ ]
		// [ ][ ][x]
		// [K][ ][ ]

		boardModel.SetCellOwnership(1, 2, oppositeTeam);
		Assert.IsFalse(boardModel.IsMovementAllowed(1, 2, pieceModel), "Failed scenario 4 movement!");
		boardModel.SetCellOwnership(1, 2, Team.None);

		// scenario 5: invalid movement - destination out of bounds;
		// [ ][ ][ ]
		// [ ][ ][ ][x]
		// [R][ ][ ]

		Assert.IsFalse(boardModel.IsMovementAllowed(1, 3, pieceModel), "Failed scenario 4 movement!");
	}
	
	[Test]
	public void PieceMovement_Bishop()
	{
		Team team = Team.White;
		Team oppositeTeam = Team.Black;
		PieceType type = PieceType.Bishop;
		BoardModel boardModel = new BoardModel();

		PieceConfig pieceConfig = AssetDatabase.LoadAssetAtPath<PieceConfig>($"{PieceConfigPathPrefix}/BishopConfig.asset");
		Assert.IsNotNull(pieceConfig, $"Cannot find PieceConfig ScriptableObject on {PieceConfigPathPrefix}!");
		
		PieceModel pieceModel = new PieceModel(team, type, pieceConfig, 0, 0);
		boardModel.SetCellOwnership(0, 0, team);

		// scenario 1: valid movement;
		// [ ][ ][x]
		// [ ][ ][ ]
		// [B][ ][ ]

		Assert.IsTrue(boardModel.IsMovementAllowed(2, 2, pieceModel), "Failed scenario 1 movement!");

		// [B][ ][ ]
		// [ ][ ][ ]
		// [ ][ ][x]
		pieceModel.Place(2, 0);
		Assert.IsTrue(boardModel.IsMovementAllowed(0, 2, pieceModel), "Failed scenario 1 movement!");
		pieceModel.Place(0, 0);

		// scenario 2: invalid movement - out of pattern;
		// [ ][ ][ ]
		// [ ][ ][ ]
		// [B][ ][x]

		Assert.IsFalse(boardModel.IsMovementAllowed(0, 2, pieceModel), "Failed scenario 2 movement!");

		// scenario 3: invalid movement - blocked path;
		// [ ][ ][x]
		// [ ][O][ ]
		// [B][ ][ ]

		boardModel.SetCellOwnership(1, 1, oppositeTeam);
		Assert.IsFalse(boardModel.IsMovementAllowed(2, 2, pieceModel), "Failed scenario 3 movement!");
		boardModel.SetCellOwnership(1, 1, Team.None);

		// scenario 4: invalid movement - blocked destination;
		// [ ][ ][O]
		// [ ][ ][ ]
		// [B][ ][ ]

		boardModel.SetCellOwnership(2, 2, oppositeTeam);
		Assert.IsFalse(boardModel.IsMovementAllowed(2, 2, pieceModel), "Failed scenario 4 movement!");
		boardModel.SetCellOwnership(2, 2, Team.None);

		// scenario 5: invalid movement - destination out of bounds;
		// [ ][ ][ ][x]
		// [ ][ ][ ]
		// [B][ ][ ]

		Assert.IsFalse(boardModel.IsMovementAllowed(2, 3, pieceModel), "Failed scenario 4 movement!");
	}

	[Test]
	public void PieceMovement_Rook()
	{
		Team team = Team.White;
		Team oppositeTeam = Team.Black;
		PieceType type = PieceType.Rook;
		BoardModel boardModel = new BoardModel();

		PieceConfig pieceConfig = AssetDatabase.LoadAssetAtPath<PieceConfig>($"{PieceConfigPathPrefix}/RookConfig.asset");
		Assert.IsNotNull(pieceConfig, $"Cannot find PieceConfig ScriptableObject on {PieceConfigPathPrefix}!");
		
		PieceModel pieceModel = new PieceModel(team, type, pieceConfig, 0, 0);
		boardModel.SetCellOwnership(0, 0, team);

		// scenario 1: valid movement;
		// [x][ ][ ]
		// [ ][ ][ ]
		// [R][ ][x]

		Assert.IsTrue(boardModel.IsMovementAllowed(0, 2, pieceModel), "Failed scenario 1 movement!");
		Assert.IsTrue(boardModel.IsMovementAllowed(2, 0, pieceModel), "Failed scenario 1 movement!");

		// scenario 2: invalid movement - out of pattern;
		// [ ][ ][x]
		// [ ][ ][ ]
		// [R][ ][ ]

		Assert.IsFalse(boardModel.IsMovementAllowed(2, 2, pieceModel), "Failed scenario 2 movement!");

		// scenario 3: invalid movement - blocked path;
		// [ ][ ][ ]
		// [ ][ ][ ]
		// [R][O][x]

		boardModel.SetCellOwnership(0, 1, oppositeTeam);
		Assert.IsFalse(boardModel.IsMovementAllowed(0, 2, pieceModel), "Failed scenario 3 movement!");
		boardModel.SetCellOwnership(0, 1, Team.None);

		// scenario 4: invalid movement - blocked destination;
		// [ ][ ][ ]
		// [ ][ ][ ]
		// [R][ ][O]

		boardModel.SetCellOwnership(0, 2, oppositeTeam);
		Assert.IsFalse(boardModel.IsMovementAllowed(0, 2, pieceModel), "Failed scenario 4 movement!");
		boardModel.SetCellOwnership(0, 2, Team.None);

		// scenario 5: invalid movement - destination out of bounds;
		// [ ][ ][ ]
		// [ ][ ][ ]
		// [R][ ][ ][x]

		Assert.IsFalse(boardModel.IsMovementAllowed(0, 3, pieceModel), "Failed scenario 4 movement!");
	}
}